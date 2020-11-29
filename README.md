# OpenSntpServer
This is a simple implementation of the sntp protocol.
A simple asynchronous server example is provided in the Program.cs and it can be run directly after compiling.

To test if the server is functioning on windows use the cmd with the following command while the server is running:

`w32tm /stripchart /computer:127.0.0.1`

also make sure the port 123 is available and shutdown any service or application which uses that port.
To find out if port 123 is already in use you can use the command: `netstat -a -n -o` and watch out for udp on port 123 and optionally close the associated pid.
