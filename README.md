# GoodHamburger

## Development description

I didn't have difficult to undestand sintax C#, i thought documentation much enlightening, i found dotnet a little bit strange, but reading documentation and developmenting application, i was can undestand, but i know i have a lot to learn.

About architecture i didn't have problems and i understood business rules.

In general, i thinking interessant C# and excited to explore more.

## Dependencies

* OS: `Linux \ Mac \ Windows`
* .NET 8.0

## How to use

In CLI execute the commando to initialize application:
```bash
dotnet run
```

Visit the next address for see UI Swagger: http://localhost:5115/swagger/index.html

Swagger disponibilize a UI interactive to test API endpoints, it is similar to Postman, you can use both.

Endpoints:

| Endpoint    | Method | Body | Description |
| -------- | ------- | ------- | ------- |
| /api/Menu  |  GET  |  | List all sandwiches and extras
| /api/Sandwich |  GET  | | List all sandwiches
| /api/Extra    | GET  | | List all extras
| /api/Order  |  POST  | `{ "SandwichId": int \| null, "extrasIds": Array<int> \| null }` | Create a order and return total price
| /api/Order  | GET | List all oders | List all orders
| /api/Order/{id}  |  PATCH  | `{ "SandwichId": int \| null, "extrasIds": Array<int> \| null }` | update a order
| /api/Order/{id}  |  DELETE  | delete a order | delete a order



