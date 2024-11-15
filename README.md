# Sunergizer-API

Projeto de API para a global solution.

## Integrantes

- Leonardo Cordeiro Scotti - RM550769
- Gabriel de Andrade Baltazar - RM550870
- Enzo Ross Gallone - RM551754
- Vinicius de Araujo Camargo - RM99494
- Pedro Gomes Fernandes - RM551480

## Descrição da Solução

A **Sunergizer-API** é uma aplicação backend construída com a arquitetura monolítica, que centraliza todos os componentes (controladores, serviços, repositórios, banco de dados) em uma única solução. O foco do projeto é fornecer funcionalidades para o gerenciamento de **Usuarios**, **Consumos**, **Comunidades** e **Fontes de Energia**, além de integrar um sistema de IA para sugestões de otimizar o consumo de energia.

O sistema permite cadastrar, buscar, atualizar e excluir informações das entidades. A IA de sugestão utiliza dados inseridos no sistema para gerar dicas para melhorar o consumo de energia de determinado usuario.

## Escolha da Arquitetura

A arquitetura monolítica foi escolhida devido à sua simplicidade de gestão, centralização de lógica de negócio e fácil escalabilidade no caso de futuras funcionalidades. O uso de um único repositório facilita o controle de versão e integrações.

## Arquitetura do Projeto

- **Controllers**: Gerenciam as requisições HTTP e rotear para as camadas de serviço.
- **Services**: Implementam a lógica de negócio da aplicação, separando-a das demais camadas.
- **Database**: Contém o contexto de dados (`SunergizerDBContext`), gerenciando o acesso ao banco.
- **Models**: Definem as entidades do sistema (ex.: Usuario, Consumo).
- **Mappings**: Realizam a conversão entre objetos de domínio e DTOs.
- **Tests**: Contém os testes unitários que garantem o funcionamento da aplicação.

## Design Patterns Utilizados

- **Repository**: Implementado na camada de banco de dados, abstraindo o acesso a dados.
- **Dependency Injection**: Utilizado para facilitar a troca de implementações, promovendo flexibilidade.
- **DTO (Data Transfer Object)**: Usado para transferência de dados entre camadas, representado nas operações de mapeamento.

## Como Rodar a API

Para rodar a API:

1. Clone ou abra o repositório na IDE Visual Studio.
2. Compile o projeto e inicie a execução.
3. Após a inicialização, você poderá acessar a API no navegador, onde serão listados os endpoints disponíveis para realizar operações CRUD nas entidades.

Para interagir com os endpoints, basta clicar no botão "Try it out", inserir os parâmetros requeridos e clicar em "Execute" para fazer as requisições. O sistema retornará um código HTTP, como **200 OK** ou **500 Internal Server Error**, indicando o status da operação.

## Testes Implementados

Foram implementados testes unitários para as funcionalidades principais do sistema, incluindo a lógica de CRUD nas entidades. Os testes são realizados nas camadas de serviço, garantindo a qualidade da lógica de negócio e o correto funcionamento dos endpoints.

## Práticas de Clean Code Utilizadas

O projeto segue as melhores práticas de **Clean Code**, incluindo:

- Funções com uma única responsabilidade.
- Nomes significativos em variáveis, métodos e classes.
- Utilização adequada de espaçamento vertical para separar conceitos.
- Código sem repetições, evitando redundâncias.
- Remoção de comentários desnecessários e sem significado.
- Nenhum efeito colateral indesejado.

## Funcionalidades da IA

A IA implementada tem como função dar sugestões para melhorar e otimizar o consumo de energia de determinado usuario.

- Consumo menor do que 100kwh retorna a mensagem: **"Consumo eficiente, mantenha o bom trabalho!"**
- Consumo menor do que 300kwh retorna a mensagem: **"Consumo médio, considere otimizar o uso."**
- Consumo maior do que 300kwh retorna a mensagem: **"Consumo alto, avalie alternativas para redução."**
