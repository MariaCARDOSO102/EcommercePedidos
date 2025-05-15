# Projeto E-commerce com Padrões de Projeto

### Contextualização do Projeto

- Este projeto tem como objetivo controlar os pedidos realizados em um sistema de e-commerce. Cada pedido possui um **status**, que pode mudar conforme o andamento da compra, e essas mudanças devem seguir regras específicas de validação. Além disso, cada pedido inclui um tipo de frete, que pode ser terrestre ou aéreo, sendo necessário calcular o valor do frete com base nessa escolha.
- Para lidar com as mudanças de status  (*Aguardando Pagamento*, *Pago*, *Enviado* ou *Cancelado)*, foi utilizado o **padrão de projeto State**, que permite alterar o comportamento do pedido conforme seu estado atual, de maneira clara e segura.
- Já o cálculo do frete, que depende do tipo de envio escolhido e precisa estar preparado para receber novas opções no futuro, foi resolvido com o uso do **padrão Strategy**. Esse padrão permite organizar diferentes formas de cálculo de maneira independente, facilitando a manutenção e a expansão do sistema.

## Implementação dos Padrões de Projeto

### Padrão State

- O padrão **State** permite que um objeto altere seu comportamento de acordo com o seu estado interno. O State foi utilizado para gerenciar as mudanças de status do pedido. A aplicação desse padrão foi essencial para manter o código organizado e evitar estruturas complexas.
- A implementação foi feita por meio de uma interface que declara os métodos de mudança de estado, sendo que cada estado é representado por uma classe específica.

### Padrão Strategy

- O padrão **Strategy** permite definir uma família de algoritmos em classes separadas, tornando seus objetos intercambiáveis. O Strategy foi aplicado para resolver o cálculo do frete, que varia conforme o tipo de envio escolhido, e pode ser necessário incluir novas formas no futuro. Por isso, esse padrão foi uma escolha estratégica para garantir a flexibilidade e a extensibilidade da aplicação.
- O cálculo do frete foi implementado por meio de uma interface que assina o método responsável pelo cálculo. Para cada tipo de frete, foi criada uma classe específica que contém a lógica correspondente. Essas classes seguem a mesma interface e encapsulam os cálculos de forma independente. Dessa maneira, é possível definir o tipo de frete em tempo de execução, mantendo a flexibilidade do sistema. Como cada estratégia de frete está isolada em sua própria classe, novas formas de envio podem ser adicionadas sem alterar o código existente. Isso garante um código mais organizado e alinhado com os princípios de responsabilidade única e aberto/fechado do SOLID.

## Estrutura da Classe Modelo

Para compreender o funcionamento do sistema como um todo, é essencial entender a estrutura da classe modelo e os atributos que compõem um pedido.

A classe modelo `Pedido.cs`, localizada na pasta `Models`, foi criada com o objetivo de representar de forma simples e objetiva um pedido realizado no sistema de E-commerce. Ela concentra as informações principais de um pedido, facilitando o controle de status e o cálculo do frete. Seus atributos são:

- **Id**: Identificador único do pedido.
- **Produto**: Nome do produto adquirido.
- **Valor**: Representa o valor do frete.
- **Subtotal**: Valor do produto, sem incluir o frete.
- **StatusPedido**: Representa o estado atual do pedido.
- **TipoFrete**: Define o tipo de frete escolhido para o envio.

Além disso, dois **enums** foram utilizados para representar dados fixos e facilitar o controle de regras no sistema:

- **StatusPedido**: Define os possíveis estados de um pedido:
    - `AguardandoPagamento`
    - `Pago`
    - `Enviado`
    - `Cancelado`
- **TipoFrete**: Define os tipos de envio disponíveis no sistema:
    - `Aereo`
    - `Terrestre`

## Lógica de Negócio na Camada Service

A camada **Service** do projeto é responsável por implementar a lógica de negócio relacionada ao gerenciamento dos pedidos. A principal classe dessa camada é o `PedidoService`, que implementa a interface `IPedidoService` e coordena operações como criação, atualização, consulta e mudanças de status dos pedidos.

**Principais responsabilidades e funcionamento:**

- **Interação com o repositório:** O `PedidoService` depende do `IPedidoRepository` para acessar e persistir dados dos pedidos no banco. Ele orquestra as operações para garantir que as regras de negócio sejam aplicadas antes de qualquer ação de armazenamento.
- **Conversão entre modelos:** Para manter o desacoplamento entre as camadas, o serviço converte entidades do banco (`Pedido`) para objetos de transferência de dados (`PedidoDTO`) e vice-versa. Isso facilita o transporte de dados e a manutenção da arquitetura limpa.
- **Gerenciamento do cálculo do frete:** O serviço utiliza o padrão **Strategy** para calcular o frete conforme o tipo escolhido (`TipoFrete`). A partir do tipo, ele instancia a estratégia correta (`FreteAereo` ou `FreteTerrestre`) que calcula o valor do frete de forma independente, permitindo fácil extensão para novos tipos no futuro.
- **Controle do ciclo de vida do pedido:** Para gerenciar os estados do pedido (como aguardando pagamento, pago, enviado, cancelado), o serviço adota o padrão **State**. Através da interface `IEstadoPedido` e suas implementações concretas (como `AguardandoPagamento`, `Pago` etc.), o serviço define as regras para transição de status, garantindo que apenas mudanças válidas sejam aplicadas (exemplo: não permitir despacho antes do pagamento).
- **Validações e regras de negócio:**
    - Ao gerar um novo pedido, o status é inicializado como `AguardandoPagamento`.
    - Atualizações só são permitidas enquanto o pedido estiver nesse status; após pagamento, cancelamento ou envio, as modificações são bloqueadas.
    - As transições de estado são controladas para evitar operações inválidas, lançando exceções quando tentativas impróprias são feitas (exemplo: tentar despachar um pedido não pago).
- **Operações disponíveis:** O serviço expõe métodos para listar todos os pedidos, buscar por ID, criar, atualizar, confirmar pagamento, despachar e cancelar pedidos, sempre aplicando as regras de negócio associadas.

## Estrutura Geral do Projeto

---

O projeto foi desenvolvido utilizando a **plataforma .NET 6** com a linguagem **C#**, estruturado como uma **API RESTful** com **ASP.NET Core**. Para a camada de persistência, foi utilizado o **Entity Framework Core**, que também gerencia as **migrations** do banco de dados. A aplicação adota princípios de **arquitetura limpa** e os conceitos do **SOLID**, além da aplicação dos padrões de projeto **State** e **Strategy** para controle de estado e cálculo de frete, respectivamente.

De maneira resumida e compacta, a estrutura geral do projeto é composta por:

- **Controllers**: Contém o `ControllerPedido.cs`, responsável por expor os endpoints da API para manipulação dos pedidos.
- **Data**: Agrupa os componentes relacionados à persistência de dados. Está subdividida em:
    - `Builders`: Configurações adicionais para a construção dos objetos.
    - `Interfaces`: Interfaces de repositórios que definem os contratos de acesso aos dados.
    - `Repositories`: Implementações dos repositórios.
    - `AppDbContext.cs`: Responsável por gerenciar a conexão com o banco de dados.
- **Migrations**: Contém os arquivos de migração gerados pelo Entity Framework, permitindo o versionamento e controle do banco de dados.
- **Objects**: Engloba os objetos do domínio da aplicação:
    - `Dtos`: Inclui a classe `PedidoDTO`, usada para o transporte de dados entre camadas.
    - `Mappings`: Realiza o mapeamento entre entidades e DTOs.
    - `Enums`: Define os enumeradores `StatusPedido` e `TipoFrete`, utilizados no controle de status e tipo de frete.
    - `Models`: Contém a classe `Pedido.cs`, a entidade principal do sistema.
- **Service**: Responsável pela lógica de negócios da aplicação:
    - `Entities`: Contém a classe `PedidoService.cs`, que implementa as regras da aplicação.
    - `Interfaces`: Interface `IPedidoService.cs`, que define o contrato dos serviços.
    - `States`: Implementação do padrão **State**, com uma interface e classes específicas para cada estado do pedido.
    - `Strategies`: Implementação do padrão **Strategy**, com a interface e classes para os diferentes tipos de frete.
- **Arquivos de configuração**: Como `appsettings.json` e `Program.cs`, essenciais para a inicialização e configuração da aplicação.
