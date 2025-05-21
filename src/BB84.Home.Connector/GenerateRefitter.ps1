refitter "https://localhost:7235/swagger/1.0/swagger.json" `
 --namespace "BB84.Home.Connector.Abstractions" `
 --output ./Abstractions `
 --contracts-namespace "BB84.Home.Conector.Contracts" `
 --contracts-output ./Contracts `
 --cancellation-tokens `
 --use-iso-date-format `
 --operation-name-template '{operationName}Async' `
 --immutable-records