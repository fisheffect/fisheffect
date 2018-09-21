---
id: interfacing_web
title: Javascript for the Web
sidebar_label: Javascript for the Web
---



Foi utilizado no projeto Neon-JS.

## Connecting to your private network;
Todas as chamadas relativas ao NEO está no arquivo neoFetcher, para utilizar private network, informe a URL do seed no atributo contractPath.

## Read only operations;
O metódo disponível para leitura é: "name". Essa chamada não requer assinatura do usuário para execução. O resultado do método é exibido no console. neoFetcher.name

## Writing to the blockchain;
O método para escrita no blockchain é: "feedReef", essa chamad requer assinatura do usuário para validar a autenticação no smart contract. Para autenticar é utilizado o metódo doInvoke.
