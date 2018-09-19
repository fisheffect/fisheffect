//
//  LoginViewController.swift
//  FishEffect
//
//  Created by Ricardo Kobayashi on 19/09/18.
//  Copyright © 2018 FishEffect. All rights reserved.
//

import UIKit
import NeoSwift

class LoginViewController: UIViewController {

    @IBOutlet weak var uiContentView: UIView!
    @IBOutlet weak var txtPassphrase: UITextField!
    @IBOutlet weak var txtEncryptedKey: UITextField!
    @IBOutlet weak var uiLoadingView: UIView!
    @IBOutlet weak var imgLoad: UIImageView!
    @IBOutlet weak var lblTitle: UILabel!
    
    var stopLoading = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.uiContentView.layer.cornerRadius = 5
        self.txtPassphrase.becomeFirstResponder()
    }

    @IBAction func loginTap(_ sender: Any) {
        self.view.endEditing(true)
        var contentCenter = self.uiContentView.center
        contentCenter.y = self.view.center.y
        
        var titleFrame = self.lblTitle.frame
        titleFrame.origin.y = contentCenter.y - (self.uiContentView.frame.size.height / 2) - self.lblTitle.frame.size.height
        
        UIView.animate(withDuration: 0.3) {
            self.uiContentView.center = contentCenter
            self.uiLoadingView.alpha = 1
            self.lblTitle.frame = titleFrame
        }
        
        var account: Account?
        if let passphrase = self.txtPassphrase.text, let encryptedKey = self.txtEncryptedKey.text, !passphrase.isEmpty && !encryptedKey.isEmpty {
            showLoadingAnimation()
            DispatchQueue.global(qos: .background).async {
                account = Account(encryptedPrivateKey: encryptedKey, passphrase: passphrase)
                if account == nil {
                    account = Account()
                }
                DispatchQueue.main.async {
                    self.stopLoading = true
                    let controller = InteractionViewController()
                    controller.account = account
                    self.navigationController?.pushViewController(controller, animated: false)
                }
            }
        }
        else {
            account = Account()
            let controller = InteractionViewController()
            controller.account = account
            self.navigationController?.pushViewController(controller, animated: false)
        }
    }
    
    func showLoadingAnimation() {
        if self.stopLoading {
            return
        }
        var newFrame = self.imgLoad.frame
        newFrame.origin.x = self.view.frame.size.width + 126
        UIView.animate(withDuration: 2.0, animations: {
            self.imgLoad.frame = newFrame
        }) { (success) in
            DispatchQueue.main.async {
                newFrame.origin.x = -126
                self.imgLoad.frame = newFrame
                self.showLoadingAnimation()
            }
        }
    }
}
