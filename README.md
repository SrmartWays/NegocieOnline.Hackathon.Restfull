## Processo de criação da tabela Debts
```sql
CREATE TABLE Debts (
    dealId INT PRIMARY KEY,          -- Código da Dívida
    value DECIMAL(10, 2),           -- Valor da dívida sem o desconto
    discount DECIMAL(10, 2),        -- Desconto da dívida
    totalValue DECIMAL(10, 2),      -- Valor total da dívida com o desconto
    customerId VARCHAR(255),        -- Código do cliente (CPF) como string
    paymentId INT,                   -- Código do pagamento
    isPaid TINYINT                   -- Indicador se a dívida foi paga (1 para pago, 0 para não pago)
);

```
## Exemplo de povoamento da tabela
```sql
INSERT INTO Debts (dealId, value, discount, totalValue, customerId, paymentId, isPaid)
VALUES
    (1, 1000.00, 200.00, 800.00, '12345678901', 101, 0),
    (2, 500.00, 100.00, 400.00, '12345678902', 102, 0);
```

 ## Como configurar a string de conexão do banco de dados com a api.
1. Abra o arquivo `appsettings.json`, onde normalmente a string de conexão é armazenada. A seção relevante pode se parecer com isto:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=hackathon;User=root;Password=root;"
   }
   ```

2. Dentro de `appsettings.json`, localize a chave da string de conexão que você deseja alterar. Neste exemplo, estamos usando `"DefaultConnection"`, mas sua aplicação pode ter um nome diferente.

3. Modifique a string de conexão conforme necessário. Por exemplo, se você deseja alterar o servidor, o banco de dados, o usuário ou a senha, faça as alterações apropriadas. Certifique-se de que a nova string de conexão esteja no formato correto para o banco de dados que você está usando.

4. Após fazer as alterações, salve o arquivo `appsettings.json`.

5. Em seguida, certifique-se de reiniciar sua aplicação para que as alterações na string de conexão entrem em vigor. Dependendo de como sua aplicação é executada, isso pode envolver parar e iniciar o serviço.

Certifique-se de que suas alterações na string de conexão estejam corretas e compatíveis com o banco de dados que você está usando. Isso garantirá que sua aplicação possa se conectar ao banco de dados corretamente após as alterações.
