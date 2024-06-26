1. cd to MagniseTask dir
2. open cmd
3. docker build -t imagename .
4. docker run -p 8080:8080 --name containername imagename
5. endpoints are available at http://localhost:8080/swagger/index.html
6. I used inMemory db for your convenience and seeded instruments, no setup required here