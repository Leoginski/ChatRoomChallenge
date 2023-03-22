# SimpleChatroom


## Instructions
Execute the following instructions to get the project running.

1. Create RabbitMQ.

    ``docker-compose up -d``
1. Create Database

    ``dotnet ef database update``
1. Run the Web application

    ``dotnet run SimpleChatroom``
1. Run the Worker application

    ``dotnet run SimpleChatroom.Worker``    
5. Access the local address
