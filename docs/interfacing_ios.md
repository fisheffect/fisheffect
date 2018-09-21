---
id: interfacing_ios
title: Swift for iOS
sidebar_label: Swift for iOS
---

O projeto foi baseado em NEO-Swift e projeto do O3Labs, refatoramos para o mínimo funcionamento.

## Connecting to your private network;
Para utilizar private network, basta instanciar NeoClient passando URL do seed no construtor. Neste projeto essa etapa é realizado no InteractionViewController.

## Read only operations;
Os metódos disponíveis para leituras são: "name", "symbol", "decimals", "totalSupply" e "balanceOf". Essas chamadas não requer assinatura do usuário para execução.
Nestes casos basta utilizar ScriptBuilder.pushContractInvoke passando scripthash do smart contract e o nome do método. Exemplo: NeoClient.getTokenName

## Writing to the blockchain;
O método para escrita no blockchain é: "feedReef", essa chamad requer assinatura do usuário para validar a autenticação no smart contract. Para autenticar é utilizado o metódo Accout.generateInvokeTransactionPayload.