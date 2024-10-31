using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Thirdweb;
using UnityEngine.UI;
using TMPro;
using System;

public class ConnectWalletManager : MonoBehaviour
{
    public string Address { get; private set; }

    string NFTAddressSmartContract = "0xed383F7a3644E89dFB7eB8F300D155F4c625E0Fe";

    public Button nftButton;
    public Button shopAndPlayButton;
    public TextMeshProUGUI nftClaimingStatusText;

    private void Start()
    {
        nftButton.gameObject.SetActive(false);
        shopAndPlayButton.gameObject.SetActive(false);
        nftClaimingStatusText.gameObject.SetActive(false);
    }

    public async void Login()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Debug.Log(Address);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        try
        {
            List<NFT> nftList = await contract.ERC721.GetOwned(Address);
            if (nftList.Count == 0)
            {
                nftButton.gameObject.SetActive(true);
            }
            else
            {
                shopAndPlayButton.gameObject.SetActive(true);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while fetching NFTs: {ex.Message}");
            // Handle the error, e.g., show an error message to the user or retry the operation
        }
    }

    public async void ClaimNFTPass()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        nftClaimingStatusText.text = "Claiming...";
        nftClaimingStatusText.gameObject.SetActive(true);
        nftButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        try
        {
            var result = await contract.ERC721.ClaimTo(Address, 1);
            nftClaimingStatusText.text = "Claimed NFT Pass!";
            nftButton.gameObject.SetActive(false);
            shopAndPlayButton.gameObject.SetActive(true);
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while claiming the NFT: {ex.Message}");
            // Optionally, update the UI to inform the user of the error
            nftClaimingStatusText.text = "Failed to claim NFT. Please try again.";
            nftButton.interactable = true;
        }
    }

    public void ToShopAndPlayScene() {
        SceneManager.LoadScene("ShopAndPlay");
    }
}
