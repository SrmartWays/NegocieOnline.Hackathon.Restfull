## Processo de criação da tabela Debts
```sql
CREATE TABLE Debts (
    dealId INT PRIMARY KEY,          -- Código da Dívida
    value DECIMAL(10, 2),           -- Valor da dívida sem o desconto
    discount DECIMAL(10, 2),        -- Desconto da dívida
    totalValue DECIMAL(10, 2),      -- Valor total da dívida com o desconto
    customerId VARCHAR(255),        -- Código do cliente (CPF) como string
    paymentId INT                    -- Código do pagamento
);
```
## Exemplo de povoamento da tabela
```sql
INSERT INTO Debts (dealId, value, discount, totalValue, customerId, paymentId)
VALUES
    (1, 1000.00, 200.00, 800.00, '12345678901', 101),
    (2, 500.00, 100.00, 400.00, '12345678902', 102);
```
