const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:44355",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
