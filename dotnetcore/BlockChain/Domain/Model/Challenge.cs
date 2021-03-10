namespace BlockChain.Domain.Model
{
    public class Challenge
    {
        public ProofOfWork Solve(ProofOfWork lastProof)
        {
            var proof = 0;
            ProofOfWork pow;
            while (!(pow = new ProofOfWork(proof)).Verify(lastProof))
                proof += 1;
            return pow;
        }   
    }
}