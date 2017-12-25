import miniupnpc


upnpc = miniupnpc.UPnP()
upnpc.discoverdelay = 200
ndevices = upnpc.discover()
print('%d UPNP device(s) detected', ndevices)

upnpc.selectigd()
external_ip = upnpc.externalipaddress()
print('external ip: %s', external_ip)
print('status: %s, connection type: %s',
      upnpc.statusinfo(),
      upnpc.connectiontype())

# find a free port for the redirection
port = 8080
external_port = port
found = False

while True:
    redirect = upnpc.getspecificportmapping(external_port, 'TCP')
    if redirect is None:
        found = True
        break
    if external_port >= 65535:
        break
    external_port = external_port + 1

print('No redirect candidate %s TCP => %s port %u TCP',
      external_ip, upnpc.lanaddr, port)

print('trying to redirect %s port %u TCP => %s port %u TCP',
      external_ip, external_port, upnpc.lanaddr, port)

res = upnpc.addportmapping(external_port, 'TCP',
                           upnpc.lanaddr, port,
                           'pyethereum p2p port %u' % external_port,
                           '')

print('Success to redirect %s port %u TCP => %s port %u TCP',
      external_ip, external_port, upnpc.lanaddr, port)
