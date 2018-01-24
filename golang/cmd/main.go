package main

import (
    "flag"
    "fmt"
    "github.com/crisadamo/gochain"
    "log"
    "net/http"
    "strings"
)

func main() {
    serverPort := flag.String("port", "8000", "http port number where server will run")
    flag.Parse()

    blockchain := gochain.NewBlockchain()
    nodeID := strings.Replace(gochain.PseudoUUID(), "-", "", -1)

    log.Printf("Starting gochain HTTP Server. Listening at port %q", *serverPort)

    http.Handle("/", gochain.NewHandler(blockchain, nodeID))
    http.ListenAndServe(fmt.Sprintf(":%s", *serverPort), nil)
}
