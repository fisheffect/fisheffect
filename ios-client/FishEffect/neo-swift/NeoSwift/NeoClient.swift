//
//  NeoClient.swift
//  NeoSwift
//
//  Created by Andrei Terentiev on 8/19/17.
//  Copyright © 2017 drei. All rights reserved.
//

import Foundation
import Neoutils

typealias JSONDictionary = [String : Any]

public enum NeoClientError: Error {
    case invalidSeed, invalidBodyRequest, invalidData, invalidRequest, noInternet
    
    var localizedDescription: String {
        switch self {
        case .invalidSeed:
            return "Invalid seed"
        case .invalidBodyRequest:
            return "Invalid body Request"
        case .invalidData:
            return "Invalid response data"
        case .invalidRequest:
            return "Invalid server request"
        case .noInternet:
            return "No Internet connection"
        }
    }
}

public enum NeoClientResult<T> {
    case success(T)
    case failure(NeoClientError)
}

public enum Network: String {
    case test
    case main
    case privateNet
}

public class NeoClient {
    public var seed = "http://seed3.o3node.org:10332"
    private init() {}
    
    enum RPCMethod: String {
        case getTransaction = "getrawtransaction"
        case getTransactionOutput = "gettxout"
        case getUnconfirmedTransactions = "getrawmempool"
        case sendTransaction = "sendrawtransaction"
        case validateAddress = "validateaddress"
        case getAccountState = "getaccountstate"
        case getAssetState = "getassetstate"
        case invokeContract = "invokescript"
        //The following routes can't be invoked by calling an RPC server
        //We must use the wrapper for the nodes made by COZ
        case getBalance = "getbalance"
    }
    
    enum NEP5Method: String {
        case balanceOf = "balanceOf"
        case decimal = "decimal"
        case symbol = "symbol"
    }
    
    enum apiURL: String {
        case getUTXO = "utxo"
        case getClaims = "claimablegas"
        case getTransactionHistory = "address/history/"
    }
    
    public init(seedURL: String) {
        seed = seedURL
    }
    
    func sendRequest(_ method: RPCMethod, params: [Any]?, completion: @escaping (NeoClientResult<JSONDictionary>) -> ()) {
        guard let url = URL(string: seed) else {
            completion(.failure(.invalidSeed))
            return
        }
        
        let request = NSMutableURLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("application/json-rpc", forHTTPHeaderField: "Content-Type")
        request.cachePolicy = .reloadIgnoringLocalCacheData
        
        let requestDictionary: [String: Any?] = [
            "jsonrpc" : "2.0",
            "id"      : 2,
            "method"  : method.rawValue,
            "params"  : params ?? []
        ]
        
        guard let body = try? JSONSerialization.data(withJSONObject: requestDictionary, options: []) else {
            completion(.failure(.invalidBodyRequest))
            return
        }
        request.httpBody = body
        
        let task = URLSession.shared.dataTask(with: request as URLRequest) { (data, _, err) in
            if err != nil {
                completion(.failure(.invalidRequest))
                return
            }
            
            guard let json = try? JSONSerialization.jsonObject(with: data!, options: []) as! JSONDictionary else {
                completion(.failure(.invalidData))
                return
            }
            
            let result = NeoClientResult.success(json)
            completion(result)
        }
        task.resume()
    }
    
    func sendFullNodeRequest(_ url: String, params: [Any]?, completion :@escaping (NeoClientResult<JSONDictionary>) -> ()) {
        let request = NSMutableURLRequest(url: URL(string: url)!)
        request.httpMethod = "GET"
        request.timeoutInterval = 60
        request.cachePolicy = .reloadIgnoringLocalCacheData
        
        let task = URLSession.shared.dataTask(with: request as URLRequest) { (data, _, err) in
            if err != nil {
                completion(.failure(.invalidRequest))
                return
            }
            
            guard let json = try? JSONSerialization.jsonObject(with: data!, options: []) as! JSONDictionary else {
                completion(.failure(.invalidData))
                return
            }
            
            let result = NeoClientResult.success(json)
            completion(result)
        }
        task.resume()
    }
    
    public func getTransaction(for hash: String, completion: @escaping (NeoClientResult<Transaction>) -> ()) {
        sendRequest(.getTransaction, params: [hash, 1]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                let decoder = JSONDecoder()
                guard let data = try? JSONSerialization.data(withJSONObject: (response["result"] as! JSONDictionary), options: .prettyPrinted),
                    let block = try? decoder.decode(Transaction.self, from: data) else {
                        completion(.failure(.invalidData))
                        return
                }
                
                let result = NeoClientResult.success(block)
                completion(result)
            }
        }
    }
    
    //NEED TO GUARD ON THE VALUE OUTS
    public func getTransactionOutput(with hash: String, and index: Int64, completion: @escaping (NeoClientResult<ValueOut>) -> ()) {
        sendRequest(.getTransaction, params: [hash, index]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                let decoder = JSONDecoder()
                guard let data = try? JSONSerialization.data(withJSONObject: (response["result"] as! JSONDictionary), options: .prettyPrinted),
                    let block = try? decoder.decode(ValueOut.self, from: data) else {
                        completion(.failure(.invalidData))
                        return
                }
                
                let result = NeoClientResult.success(block)
                completion(result)
            }
        }
    }
    
    public func getUnconfirmedTransactions(completion: @escaping (NeoClientResult<[String]>) -> ()) {
        sendRequest(.getUnconfirmedTransactions, params: nil) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                guard let txs = response["result"] as? [String] else {
                    completion(.failure(.invalidData))
                    return
                }
                
                let result = NeoClientResult.success(txs)
                completion(result)
            }
        }
    }
    
    public func sendRawTransaction(with data: Data, completion: @escaping(NeoClientResult<Bool>) -> ()) {
        sendRequest(.sendTransaction, params: [data.fullHexString]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                guard let success = response["result"] as? Bool else {
                    completion(.failure(.invalidData))
                    return
                }
                let result = NeoClientResult.success(success)
                completion(result)
            }
        }
    }
    
    public func validateAddress(_ address: String, completion: @escaping(NeoClientResult<Bool>) -> ()) {
        sendRequest(.validateAddress, params: [address]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                guard let jsonResult: [String: Any] = response["result"] as? JSONDictionary else {
                    completion(.failure(.invalidData))
                    return
                }
                
                guard let isValid = jsonResult["isvalid"] as? Bool else {
                    completion(.failure(.invalidData))
                    return
                }
                
                let result = NeoClientResult.success(isValid)
                completion(result)
            }
        }
    }
    
    public func getAccountState(for address: String, completion: @escaping(NeoClientResult<AccountState>) -> ()) {
        sendRequest(.getAccountState, params: [address]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                let decoder = JSONDecoder()
                guard let data = try? JSONSerialization.data(withJSONObject: (response["result"] as! JSONDictionary), options: .prettyPrinted),
                    let accountState = try? decoder.decode(AccountState.self, from: data) else {
                        completion(.failure(.invalidData))
                        return
                }
                
                let result = NeoClientResult.success(accountState)
                completion(result)
            }
        }
    }
    
    public func sendFeedReef(with data: Data, completion: @escaping(NeoClientResult<Bool>) -> ()) {
        sendRequest(.invokeContract, params: [data.fullHexString]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                guard let success = response["result"] as? Bool else {
                    completion(.failure(.invalidData))
                    return
                }
                let result = NeoClientResult.success(success)
                completion(result)
            }
        }
    }
    
    public func invokeContract(with script: String, completion: @escaping(NeoClientResult<ContractResult>) -> ()) {
        sendRequest(.invokeContract, params: [script]) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                let decoder = JSONDecoder()
                if response["result"] == nil {
                    completion(.failure(NeoClientError.invalidData))
                    return
                }
                guard let data = try? JSONSerialization.data(withJSONObject: (response["result"] as! JSONDictionary), options: .prettyPrinted),
                    let contractResult = try? decoder.decode(ContractResult.self, from: data) else {
                        completion(.failure(.invalidData))
                        return
                }
                
                let result = NeoClientResult.success(contractResult)
                completion(result)
            }
        }
    }
    
    public func getTokenName(with scriptHash: String, completion: @escaping(NeoClientResult<String>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "name")
        invokeContract(with: scriptBuilder.rawHexString) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let contractResult):
                let nameEntry = contractResult.stack[1]
                guard let name = String(data: (nameEntry.hexDataValue?.dataWithHexString())!, encoding: .utf8) else {
                    completion(.failure(.invalidData))
                    return
                }
                completion(.success(name))
            }
        }
    }
    
    public func getTokenSymbol(with scriptHash: String, completion: @escaping(NeoClientResult<String>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "symbol")
        invokeContract(with: scriptBuilder.rawHexString) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let contractResult):
                let symbolEntry = contractResult.stack[1]
                guard let symbol = String(data: (symbolEntry.hexDataValue?.dataWithHexString())!, encoding: .utf8) else {
                    completion(.failure(.invalidData))
                    return
                }
                completion(.success(symbol))
            }
        }
    }
    
    public func getTokenDecimals(with scriptHash: String, completion: @escaping(NeoClientResult<String>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "decimals")
        invokeContract(with: scriptBuilder.rawHexString) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let contractResult):
                let decimalEntry = contractResult.stack[1]
                guard let decimal = decimalEntry.intValue else {
                    completion(.failure(.invalidData))
                    return
                }
                completion(.success(String(format:"%i", decimal)))
            }
        }
    }
    
    public func getTokenTotalSupply(with scriptHash: String, completion: @escaping(NeoClientResult<String>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "totalSupply")
        invokeContract(with: scriptBuilder.rawHexString) { result in
            switch result {
            case .failure(let error):
                completion(.failure(error))
            case .success(let contractResult):
                let totalSupplyEntry = contractResult.stack[1]
                guard let totalSupplyData = totalSupplyEntry.hexDataValue else {
                    completion(.failure(.invalidData))
                    return
                }
                let totalSupply = UInt64(littleEndian: totalSupplyData.dataWithHexString().withUnsafeBytes { $0.pointee })
                completion(.success(String(format:"%i", totalSupply)))
            }
        }
    }
    
    public func getTokenBalance(_ scriptHash: String, address: String, completion: @escaping(NeoClientResult<Double>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "balanceOf", args: [address.hashFromAddress()])
        self.invokeContract(with: scriptBuilder.rawHexString) { contractResult in
            switch contractResult {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                #if DEBUG
                print(response)
                #endif
                let balanceData = response.stack[1].hexDataValue ?? ""
                if balanceData == "" {
                    completion(.success(0))
                    return
                }
                
                let balance = Double(balanceData.littleEndianHexToUInt)
                let divider = pow(Double(10), Double(2))
                let amount = balance / divider
                completion(.success(amount))
            }
        }
    }
    
    public func transferValue(_ scriptHash: String, addressFrom: String, addressTo: String, valueInCents: Int64, completion: @escaping(NeoClientResult<Double>) -> ()) {
        let scriptBuilder = ScriptBuilder()
        
        scriptBuilder.pushContractInvoke(scriptHash: scriptHash, operation: "transfer", args: [Int(10 * 100000000), addressTo.hashFromAddress(), addressFrom.hashFromAddress()])
        
        let script = scriptBuilder.rawBytes
        let scriptBytes = [UInt8(script.count)] + script
        
        self.invokeContract(with: scriptBytes.fullHexString) { contractResult in
            switch contractResult {
            case .failure(let error):
                completion(.failure(error))
            case .success(let response):
                print(response)
                completion(.success(0.0))
            }
        }
    }
}
