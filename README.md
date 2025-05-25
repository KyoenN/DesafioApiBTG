# DesafioApiBTG
Repositório para entrega do desafio técnico do BTG

## Summary
* [Objetivo](#Objetivo)
* [Descrição](#Descrição)
* [Frameworks e Ferramentas](#frameworks)
* [Setup](#setup)

## Objetivo
* Desenvolver um sistema backend simples, com arquitetura bem definida, que simule o processamento assíncrono de pedidos utilizando uma fila de mensagens. O sistema deve expor uma API HTTP para criação e consulta de pedidos, e utilizar uma fila (Kafka, RabbitMQ, MQTT, ou outra de sua preferência) para simular o fluxo de processamento dos pedidos.

## Descrição
* Você deve implementar um serviço que permita:    
1. __Criar um novo pedido__ via uma requisição HTTP POST /pedidos.
   - O corpo da requisição deve conter um identificador do cliente e uma lista de itens.
   -  Ao receber o pedido, o sistema deve gerar um ID único para ele e publicá-lo em uma fila de mensagens para processamento posterior.
2. __Simular o processamento do pedido:__
   - Um consumidor da fila deve processar os pedidos recebidos com um pequeno delay artificial (ex: 2 segundos) e marcar o pedido como "processado".
   - O status do pedido deve ser mantido em memória (não usar banco de dados).

3. __Consultar o status de um pedido via:__ GET /pedidos/{id}.

## Requisitos Técnicos
* O sistema deve ser implementado em uma linguagem de sua escolha.
* A arquitetura deve ser clara, modular e orientada a boas práticas de design de software.
* Não é necessário implementar autenticação, banco de dados ou interface gráfica.
* O uso de bibliotecas/frameworks é livre, desde que justificado.
* O sistema deve ser executável localmente com instruções claras de como rodar.

## Frameworks e Ferramentas
Projeto feito em C# utilizando .NET CORE / ASP.NET CORE com o RabbitMQ para mensageria.
* RabbitMQ: 7.1.2
* .NET 8

## Setup
Esse projeto requer __um node do RabbitMQ rodando no localhost com as configurações padrão(default)__ para funcionar.  Para testes e validação foi utilizado a imagem comunitária do RabbitMQ no Docker.
* Instalação do Docker Desktop em https://docs.docker.com/desktop/

* Instalar o .NET 8 Runtime em https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/8.0.16/windowsdesktop-runtime-8.0.16-win-x64.exe
* Dentro deste repositório se encontram os executáveis: __MockProcessingNode__ que processa a requisição da aplicação API e __APIDesafio__ que implementa os EndPoints requeridos.
* Para executar esse projeto inicie o node do RabbitMQ executando o comando `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management` no Console/PowerShell e em seguida execute ambos os executáveis.
* Para visualizar as Filas do RabbitMQ acesse localhost:15672/
*  __As credenciais Padrão do RabbitMQ são: user = guest password = guest.__
* Para interagir com os EndPoints acesse localhost:5000/swagger
* É possível também visualizar o tráfego de mensagens pela janela de console de ambas aplicações.
  
 


