const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:44356",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
