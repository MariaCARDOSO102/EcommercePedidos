# Projeto E-commerce com Padrões de Projeto

## Contextualização do Projeto

Este projeto tem como objetivo controlar os pedidos realizados em um sistema de e-commerce. Cada pedido possui um **status**, que pode mudar conforme o andamento da compra, seguindo regras específicas de validação. Além disso, cada pedido inclui um tipo de frete (terrestre ou aéreo), sendo necessário calcular o valor do frete com base nessa escolha.

- Para lidar com as mudanças de status (*Aguardando Pagamento*, *Pago*, *Enviado* ou *Cancelado*), foi utilizado o **padrão de projeto State**, que permite alterar o comportamento do pedido conforme seu estado atual, de forma clara e segura.
- Para o cálculo do frete, que depende do tipo de envio escolhido e deve estar preparado para novas opções no futuro, foi utilizado o **padrão Strategy**, que permite organizar diferentes formas de cálculo de maneira independente, facilitando a manutenção e a expansão do sistema.

---

## Implementação dos Padrões de Projeto

### Padrão State

- O **padrão State** permite que um objeto altere seu comportamento de acordo com seu estado interno.
- Foi utilizado para gerenciar as mudanças de status do pedido, evitando estruturas complexas como grandes blocos condicionais.
- Cada estado é representado por uma classe específica que implementa uma interface comum com os métodos de transição de estado.

### Padrão Strategy

- O **padrão Strategy** define uma família de algoritmos encapsulados em classes separadas, que podem ser utilizadas de forma intercambiável.
- Aplicado ao cálculo do frete, permitindo que cada tipo de frete (aéreo ou terrestre) tenha sua própria lógica de cálculo.
- Novas estratégias podem ser adicionadas sem modificar o código existente, respeitando os princípios **SOLID** (em especial, o princípio open/closed).

---

## Estrutura da Classe Modelo

A classe `Pedido.cs`, localizada na pasta `Models`, representa um pedido no sistema de e-commerce. Seus atributos são:

- `Id`: Identificador único do pedido.
- `Produto`: Nome do produto adquirido.
- `Valor`: Valor do frete.
- `Subtotal`: Valor do produto sem frete.
- `StatusPedido`: Estado atual do pedido.
- `TipoFrete`: Tipo de frete escolhido (aéreo ou terrestre).

Além disso, dois enums foram utilizados:

- **StatusPedido**: `AguardandoPagamento`, `Pago`, `Enviado`, `Cancelado`
- **TipoFrete**: `Aereo`, `Terrestre`

### Diagrama Model
![image](https://github.com/user-attachments/assets/69764501-2ad1-4bd6-8798-8f89083ae6ec)


---

## PedidoService – Regras de Negócio do Pedido

A classe `PedidoService`, que implementa a interface `IPedidoService`, é responsável por toda a lógica de negócio da aplicação. Ela atua como ponte entre os controladores e o domínio da aplicação.

### Principais funcionalidades:

### 1. Listagem e Consulta

- `ListAll()`: Retorna todos os pedidos em formato `PedidoDTO`.
- `GetById(int id)`: Retorna um pedido por ID ou lança exceção se não encontrado.

### 2. Criação de Pedido

- `GerarPedido(PedidoDTO dto)`:
    - Transforma o DTO em entidade.
    - Define o status inicial como `AguardandoPagamento`.
    - Calcula o valor do frete usando a estratégia apropriada.
    - Persiste e retorna o pedido como `PedidoDTO`.

### 3. Atualização de Pedido

- `AtualizarEntidadeDTO(PedidoDTO dto, int id)`:
    - Permitido apenas se o pedido estiver em `AguardandoPagamento`.
    - Recalcula o frete e atualiza os dados.

### 4. Transições de Estado (State)

- `SucessoAoPagar(int id)`
- `DespacharPedido(int id)`
- `CancelarPedido(int id)`

Esses métodos:

- Recuperam o pedido atual.
- Utilizam a lógica da classe de estado correspondente (`IEstadoPedido`) para validar e aplicar a transição.

### 5. Cálculo de Frete (Strategy)

- `GerarFretePorTipo(TipoFrete tipo)`:
    - Define dinamicamente a estratégia de cálculo: `FreteAereo` ou `FreteTerrestre`.

### 6. Conversão de Dados

- `ConverterParaDTO(Pedido)`: Converte a entidade para DTO.
- `ConverterParaModel(PedidoDTO)`: Converte o DTO para entidade.

## Padrões de Projeto Utilizados

### Strategy

- Interface: `IFrete`
- Implementações: `FreteTerrestre`, `FreteAereo`
- Responsável pelo cálculo de frete de forma flexível.

### State

- Interface: `IEstadoPedido`
- Estados: `AguardandoPagamento`, `Pago`, `Cancelado`, `Enviado`
- Responsável por controlar o ciclo de vida do pedido com transições seguras.

### Diagrama Service
![image](https://github.com/user-attachments/assets/d0a89916-045e-4f73-bd17-7c6970e08104)
---

## Estrutura Geral do Projeto

O projeto foi desenvolvido com **.NET 6**, utilizando **C#** e estruturado como uma **API RESTful** com **ASP.NET Core**. O **Entity Framework Core** é utilizado para persistência e controle de migrations.

### Principais diretórios:

- **Controllers**: Contém `ControllerPedido.cs`, com os endpoints da API.
- **Data**:
    - `Builders`: Configurações adicionais dos modelos.
    - `Interfaces`: Contratos dos repositórios.
    - `Repositories`: Implementações dos repositórios.
    - `AppDbContext.cs`: Gerencia a conexão e o mapeamento com o banco.
- **Migrations**: Controle de versões do banco.
- **Objects**:
    - `Dtos`: Classe `PedidoDTO`, para transporte de dados.
    - `Mappings`: Mapeamento entre entidade e DTO.
    - `Enums`: `StatusPedido` e `TipoFrete`.
    - `Models`: Classe `Pedido.cs`.
- **Service**:
    - `Entities`: `PedidoService.cs`, regras de negócio.
    - `Interfaces`: Interface `IPedidoService.cs`.
    - `States`: Implementação do padrão **State**.
    - `Strategies`: Implementação do padrão **Strategy**.
- **Configuração**:
    - `Program.cs` e `appsettings.json`.

## Conclusão

Este projeto demonstra a aplicação prática dos padrões de projeto **State** e **Strategy** para gerenciar o ciclo de vida dos pedidos e o cálculo de frete em um sistema de e-commerce. A arquitetura adotada garante código organizado, flexível e fácil de manter, facilitando futuras expansões e adaptações do sistema.
