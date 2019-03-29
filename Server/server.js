var WebSocketServer = require('ws').Server
var wss = new WebSocketServer({
  port: 8080
});

wss.on('connection', (ws) => {
  ws.on('message', (message) => {
    console.log('received: %s', message);
    ws.send(message);
  });
  ws.send('something');
});
