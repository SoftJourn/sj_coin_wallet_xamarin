### Softjourn Coin Wallet (Xamarin)
A very simple wallet application for a smart contract based coin token for Eris(Monax) blockchain server.
Set Eris(Monax) server url, select your account and scan public & private keys with the mobile phone camera.

### QR Codes
To generate public & private key into a scannable QR code, you can use Google Charts:
https://chart.googleapis.com/chart?chs=200x290&cht=qr&chl=HEXENCODEDKEY

### Install notes
1. Check/change Eris service URL first
2. Select your account address from the Address Book list
3. Scan public & private keys
Currently sending coins will be available on the next application start.

### Example contract
```
contract Coin {
    address minter;
    mapping (address => uint) balances;

    event Send(address from, address to, uint value);

    function Coin() {
        minter = msg.sender;
    }

    function mint(address owner, uint amount) {
        if (msg.sender != minter) return;
        balances[owner] += amount;
    }

    function send(address receiver, uint amount) returns (bool) {
        if (balances[msg.sender] < amount) return false;
        balances[msg.sender] -= amount;
        balances[receiver] += amount;
        Send(msg.sender, receiver, amount);
        return true;
    }

    function queryBalance(address addr) constant returns (uint balance) {
        return balances[addr];
    }
}
```
