# Onion S.A. - Sistema de Controle de Vendas

Bem-vindo ao sistema de controle de vendas da Onion S.A.! Este é um projeto de back-end desenvolvido em C# .NET 6 para tratar questões de controle de vendas e logística na empresa.

## Descrição do Sistema
O sistema permite aos usuários importar uma planilha de pedidos e visualizar informações cruciais de vendas por meio de:

- Gráfico de Vendas por Região: Apresenta visualização gráfica das vendas baseadas na distribuição regional dos pedidos.
- Gráfico de Vendas por Produto: Exibe gráficos representando as vendas por produto, oferecendo uma visão geral das preferências de compra.
- Lista de Vendas: Apresenta informações detalhadas, incluindo nome do cliente, produto, valor final com entrega e data de entrega.

## Campos da Planilha
Os campos que a planilha precisa ter são:

- CPF ou CNPJ
- Nome ou Razão Social
- CEP
- Produto
- Número do Pedido
- Data

## Telas do Sistema
O sistema possui duas telas:

### Tela de Importação de Pedidos por Planilha
Nesta tela, os usuários poderão:

- Ler uma descrição do sistema.
- Baixar um exemplo de planilha a ser preenchida.
- Importar a planilha preenchida para o sistema.

### Tela de Exibição de Dados
Nesta tela, os usuários encontrarão:

- Dois gráficos simples em forma de pizza.
- Uma listagem com os pedidos e cálculos relevantes.

## Instalação
### Pré-Requisitos
- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Passos de Instalação
1. Clone o repositório: `git clone https://github.com/leonardonunessantos/OnionSa-Api.git`
2. Acesse a pasta do projeto: `cd nome-do-repo`
3. Execute o projeto: `dotnet run`

### Back-end
- O back-end é desenvolvido em C# .NET 6, seguindo o formato de API REST.
- Utilizando a API ViaCep (https://viacep.com.br/) para consultar a localização.
- Utilização do Entity Framework e Banco de dados InMemory;
- Tabelas:
    1. Pedidos
    2. Produtos (cadastrar os 3 produtos citados)
    3. Clientes (chave única será o documento (CPF/CNPJ), sem pontos ou traços)


