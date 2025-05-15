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
