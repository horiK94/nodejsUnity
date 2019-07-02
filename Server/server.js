var WebSocketServer = require('ws').Server
var wss = new WebSocketServer({
  port: 8080
});

let playerId = 1;
wss.on('connection', (ws) => {
  ws.on('message', (message) => {
    console.log('received: %s', message);
    ws.send(message);
  });
  let sendData = {};
  sendData["id"] = playerId;
  playerId++;
  ws.send(JSON.stringify(sendData));
});
