//
//  InteractionViewController.swift
//  FishEffect
//
//  Created by Ricardo Kobayashi on 14/09/18.
//  Copyright © 2018 FishEffect. All rights reserved.
//

import UIKit
import NeoSwift

class InteractionViewController: UIViewController {

    let neoClient = NeoClient(network: .main, seedURL: "http://18.191.236.185:30333")
    let smartcontract_hash = "1ee5f347f564829378e642bc408f310953ffdc95"
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }

    @IBAction func symbolTap(_ sender: Any) {
        neoClient.getTokenSymbol(with: self.smartcontract_hash, completion: { (nep5) in
            switch nep5 {
            case .failure(let error):
                self.showGenericError(error.localizedDescription)
            case .success(let symbol):
                let alert = UIAlertController(title: nil, message: symbol, preferredStyle: UIAlertControllerStyle.alert)
                alert.addAction(UIAlertAction(title: "OK", style: .default, handler: { action in }))
                self.present(alert, animated: true, completion: nil)
            }
        })
    }
    
    @IBAction func nameTap(_ sender: Any) {
        neoClient.getTokenName(with: self.smartcontract_hash, completion: { (nep5) in
            switch nep5 {
            case .failure(let error):
                self.showGenericError(error.localizedDescription)
            case .success(let name):
                let alert = UIAlertController(title: nil, message: name, preferredStyle: UIAlertControllerStyle.alert)
                alert.addAction(UIAlertAction(title: "OK", style: .default, handler: { action in }))
                self.present(alert, animated: true, completion: nil)
            }
        })
    }
    
    @IBAction func decimalsTap(_ sender: Any) {
        neoClient.getTokenDecimals(with: self.smartcontract_hash, completion: { (nep5) in
            switch nep5 {
            case .failure(let error):
                self.showGenericError(error.localizedDescription)
            case .success(let decimals):
                let alert = UIAlertController(title: nil, message: decimals, preferredStyle: UIAlertControllerStyle.alert)
                alert.addAction(UIAlertAction(title: "OK", style: .default, handler: { action in }))
                self.present(alert, animated: true, completion: nil)
            }
        })
    }
    
    @IBAction func totalSupplyTap(_ sender: Any) {
        neoClient.getTokenTotalSupply(with: self.smartcontract_hash, completion: { (nep5) in
            switch nep5 {
            case .failure(let error):
                self.showGenericError(error.localizedDescription)
            case .success(let totalSupply):
                let alert = UIAlertController(title: nil, message: totalSupply, preferredStyle: UIAlertControllerStyle.alert)
                alert.addAction(UIAlertAction(title: "OK", style: .default, handler: { action in }))
                self.present(alert, animated: true, completion: nil)
            }
        })
    }
    
    func showGenericError(_ message: String) {
        let alert = UIAlertController(title: "Unexpected Error", message: message, preferredStyle: UIAlertControllerStyle.alert)
        alert.addAction(UIAlertAction(title: "OK", style: .default, handler: { action in }))
        self.present(alert, animated: true, completion: nil)
    }
}
