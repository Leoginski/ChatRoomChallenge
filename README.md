# SimpleChatroom


## Instructions
Execute the following instructions to get the project running.

1. Create RabbitMQ.

    ``docker-compose up -d``
1. Create Database

    ``dotnet ef -p SimpleChatroom database update``
1. Run the Web application

    ``dotnet run -p SimpleChatroom``
1. Run the Worker application in a new terminal

    ``dotnet run -p SimpleChatroom.Worker``    
5. Access the local address

## Mandatory Features
- [x] Allow registered users to log in and talk with other users in a chatroom.
- [x] Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code
- [x] Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code)
- [x] The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.
- [x] Have the chat messages ordered by their timestamps and show only the last 50
messages.
- [x] Unit test the functionality you prefer.

## Bonus (Optional)
- [ ] Have more than one chatroom.
- [x] Use .NET identity for users authentication
- [ ] Handle messages that are not understood or any exceptions raised within the bot.
- [ ] Build an installer.
