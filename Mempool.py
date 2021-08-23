'''The following is a procedure to select transactions from the mempool, the mempool is emulated by a csv file containing tx id, fee, weight, and parents
   A general rule of the mempool is that if a child transaction is present, all its parents should also be present. The procedure can further be integrated into the main code.
   
   Since there were multiple ways of approaching this problem to find the value closest to maximum fees collected, I used the following approach:
    
    In my approach, I initially determine the fee to size ratio of the transactions and sort them  by the descending order.
    Then I keep on filling the transactions in the same order into the block and keep updating the remaining space in the block.
    If a transaction does not fit in the block, I skip it and move forward in the descending order of fee rate until I find a transaction small enough to fit.
    
    For parents/children transactions too there are a lot of ways of approaching the problem, the one I chose was:
        
        I grouped all genetic (parent children heirarchy) family transactions together, i.e. grouped children with parents and grandparents and great grand parents
        in one single block. I added the weights and fees of all these ancestors to the combined block and treated this block as an individual block in 
        calculating the fee rate. This ensured that the transactions always stayed together and if selected for mining, and maintained their natural genetic order 
        in the lists of transactions. 
'''

#For CSV manipulation (Reading)
import csv


#Class containing all Transaction related functions
class Transactions:
    
    #List used for storing transactions from CSV file
    trans = []
    

    #Function for reading CSV file
    def readcsv(self):
        
        with open('mempool.csv','r') as mempool_file:
            
            mempool = csv.reader(mempool_file, delimiter = ',')
            
            #This was done to ignore the first row containing coloum names
            next(mempool)
            
            for row in mempool:
                
                #List for storing all parent transactions of child
                parent= []
                
                #Variable to store transaction ID
                tx = row[0]
                
                #Variable to store fees
                fee = row[1]
                
                #Variable to store weights
                wt = row[2]
                
                #condition to check and store parents in a list
                if(row[3]!=''):
                    
                    parent = row[3].split(';')
                        
                    
                #List to append in the transaction list
                tcx = []
                
                tcx.append(tx)
                tcx.append(fee)
                tcx.append(wt)
                tcx.append(parent)
                
                #appending into transactions
                self.trans.append(tcx)
        
        #Loop for combining parent/children blocks
        
        for ent in self.trans:
            
            if(len(ent[3])!=0):
                
                #print("In:", ent[0]) :Debugger I used to check instream and outstream
                
                for x in self.trans:
                    
                    for y in ent[3]:
                        
                        #If parent is found in string, it's heirarchy would be appended in the child block
                        if(x[0].find(y)!=-1 and x!=ent):
                            
                            ent[2] = int(ent[2])+int(x[2])
                            
                            ent[1] = int(ent[1])+int(x[1])
                            
                            
                            ent[0] = x[0] + ":" + ent[0]
                            
                            
                            #Removes the parent copy of the transactiom
                            self.trans.remove(x)
                            break
                            
                            
                #print("out", ent[0])
                            
        
        #Loop for calculating Fee Rate (fee/weight)

        for u in self.trans:
            
            feerate = float(int(u[1])/int(u[2]))
            
            u.append(feerate)
    
    
    #Function to select transactions and adding to text file
    
    def selectrans(self):
        
        fileobj = open("block.txt","w+")
        
        #Sorting transactions based on fee rates, high to low
        sort = sorted(self.trans,key = lambda x:x[4], reverse = True)
        
        blksize =int(0)
        
        #Loop to add transactions and updating block size
        for i in sort:
            if(blksize + int(i[2]) <= 4000000):
                
                if(len(i[0])>64):
                    chl = i[0].split(":")
                    for x in chl:
                        
                        fileobj.write(x+"\n")
                    
                else:
            
                    fileobj.write(i[0]+"\n")
                    
                blksize += int(i[2])
                
        
        fileobj.close()
        
        


obj = Transactions()

obj.readcsv()

obj.selectrans()
