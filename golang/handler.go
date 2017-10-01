package gochain

import (
    "encoding/json"
    "fmt"
    "io"
    "log"
    "net/http"
)

func NewHandler(blockchain *Blockchain, nodeID string) http.Handler {
    h := handler{blockchain, nodeID}

    mux := http.NewServeMux()
    mux.HandleFunc("/nodes/register", buildResponse(h.RegisterNode))
    mux.HandleFunc("/nodes/resolve", buildResponse(h.ResolveConflicts))
    mux.HandleFunc("/transactions/new", buildResponse(h.AddTransaction))
    mux.HandleFunc("/mine", buildResponse(h.Mine))
    mux.HandleFunc("/chain", buildResponse(h.Blockchain))
    return mux
}

type handler struct {
    blockchain *Blockchain
    nodeId     string
}

type response struct {
    value      interface{}
    statusCode int
    err        error
}

func buildResponse(h func(io.Writer, *http.Request) response) http.HandlerFunc {
    return func(w http.ResponseWriter, r *http.Request) {
        resp := h(w, r)
        msg := resp.value
        if resp.err != nil {
            msg = resp.err.Error()
        }
        w.WriteHeader(resp.statusCode)
        w.Header().Set("Content-Type", "application/json")
        if err := json.NewEncoder(w).Encode(msg); err != nil {
            log.Printf("could not encode response to output: %v", err)
        }
    }
}

func (h *handler) AddTransaction(w io.Writer, r *http.Request) response {
    if r.Method != http.MethodPost {
        return response{
            nil,
            http.StatusMethodNotAllowed,
            fmt.Errorf("method %s not allowd", r.Method),
        }
    }

    log.Printf("Adding transaction to the blockchain...\n")

    var tx Transaction
    err := json.NewDecoder(r.Body).Decode(&tx)
    index := h.blockchain.NewTransaction(tx)

    resp := map[string]string{
        "message": fmt.Sprintf("Transaction will be added to Block %d", index),
    }

    status := http.StatusCreated
    if err != nil {
        status = http.StatusInternalServerError
        log.Printf("there was an error when trying to add a transaction %v\n", err)
        err = fmt.Errorf("fail to add transaction to the blockchain")
    }

    return response{resp, status, err}
}

func (h *handler) Mine(w io.Writer, r *http.Request) response {
    if r.Method != http.MethodGet {
        return response{
            nil,
            http.StatusMethodNotAllowed,
            fmt.Errorf("method %s not allowd", r.Method),
        }
    }

    log.Println("Mining some coins")

    // We run the proof of work algorithm to get the next proof...
    lastBlock := h.blockchain.LastBlock()
    lastProof := lastBlock.Proof
    proof := h.blockchain.ProofOfWork(lastProof)

    // We must receive a reward for finding the proof.
    // The sender is "0" to signify that this node has mined a new coin.
    newTX := Transaction{Sender: "0", Recipient: h.nodeId, Amount: 1}
    h.blockchain.NewTransaction(newTX)

    // Forge the new Block by adding it to the chain
    block := h.blockchain.NewBlock(proof, "")

    resp := map[string]interface{}{"message": "New Block Forged", "block": block}
    return response{resp, http.StatusOK, nil}
}

func (h *handler) Blockchain(w io.Writer, r *http.Request) response {
    if r.Method != http.MethodGet {
        return response{
            nil,
            http.StatusMethodNotAllowed,
            fmt.Errorf("method %s not allowd", r.Method),
        }
    }
    log.Println("Blockchain requested")

    resp := map[string]interface{}{"chain": h.blockchain.chain, "length": len(h.blockchain.chain)}
    return response{resp, http.StatusOK, nil}
}

func (h *handler) RegisterNode(w io.Writer, r *http.Request) response {
    if r.Method != http.MethodPost {
        return response{
            nil,
            http.StatusMethodNotAllowed,
            fmt.Errorf("method %s not allowd", r.Method),
        }
    }

    log.Println("Adding node to the blockchain")

    var body map[string][]string
    err := json.NewDecoder(r.Body).Decode(&body)

    for _, node := range body["nodes"] {
        h.blockchain.RegisterNode(node)
    }

    resp := map[string]interface{}{
        "message": "New nodes have been added",
        "nodes":   h.blockchain.nodes.Keys(),
    }

    status := http.StatusCreated
    if err != nil {
        status = http.StatusInternalServerError
        err = fmt.Errorf("fail to register nodes")
        log.Printf("there was an error when trying to register a new node %v\n", err)
    }

    return response{resp, status, err}
}

func (h *handler) ResolveConflicts(w io.Writer, r *http.Request) response {
    if r.Method != http.MethodGet {
        return response{
            nil,
            http.StatusMethodNotAllowed,
            fmt.Errorf("method %s not allowd", r.Method),
        }
    }

    log.Println("Resolving blockchain differences by consensus")

    msg := "Our chain is authoritative"
    if h.blockchain.ResolveConflicts() {
        msg = "Our chain was replaced"
    }

    resp := map[string]interface{}{"message": msg, "chain": h.blockchain.chain}
    return response{resp, http.StatusOK, nil}
}
