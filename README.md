## Processo de criação da database
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
