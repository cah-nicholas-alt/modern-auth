# Requirements
Docker or .Net 5.0, Java 11, and Yarn vLatest

# Executing
```docker-compose up -d --build```

# Hosts
In order to demonstrate the cross domain capabilities of OAUTH and OIDC add the following host aliases to your systems host file

```127.0.0.1 id.pursuit.local accounts.api.pursuit.local mortgage.api.pursuit.local accounts.app.pursuit.local mortgage.app.pursuit.local app.coin.local```

# Applications
Pursuit Id: http://id.pursuit.local:5000

Pursuit Account App: http://accounts.app.pursuit.local:5003

Pursuit Mortgage App:

Pursuit Acount API: http://accounts.api.pursuit.local:5001

Pursuit Mortgage API: http://mortgage.api.pursuit.local:5002

Coin App: http://app.coin.local:5004

# Cookie Settings
As we are running locally running on HTTP you will need to enable the chome setting: `Cookies without SameSite must be secure` otherwise chrome may block our non-secure, non-same site cookies.