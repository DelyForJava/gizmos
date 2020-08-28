package main

import (
	"fmt"
	"log"
	"net/http"
)

func start() {
	http.Handle("/download/", http.StripPrefix("/download/", http.FileServer(http.Dir("./download"))))
	err := http.ListenAndServe(":8080", nil)
	if err != nil {
		log.Fatal("ListenAndServe:", err.Error())
	}
	fmt.Println("whatever it takes")
}

func main() {
	go start()
	select {}

}
