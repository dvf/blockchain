import miniupnpc

import logging


log = logging.getLogger('root.networking')


class PortMapper(object):
    def __init__(self):
        client = miniupnpc.UPnP()
        client.discoverdelay = 200

        try:
            log.info('Searching for Internet Gateway Devices... (timeout: %sms)', client.discoverdelay)
            device_count = client.discover()

            log.info('Found %s devices', device_count)

            self.client = client
            self.device_count = device_count
            self.internal_ip = None
            self.external_ip = None
            self.external_port = None

        except Exception as e:
            log.error('An unexpected error occurred: %s', e)
            self.client = None

    def add_portmapping(self, internal_port, external_port, protocol, label=''):
        if self.client is None:
            log.error('No uPnP devices were found on the network')
            return

        try:
            self.client.selectigd()

            self.internal_ip = self.client.lanaddr
            self.external_ip = self.client.externalipaddress()

            log.info('Internal IP: %s', self.internal_ip)
            log.info('External IP: %s', self.external_ip)

            log.info('Attempting %s redirect: %s:%s -> %s:%s', protocol,
                     self.external_ip, external_port,
                     self.internal_ip, internal_port)

            # Find an available port for the redirect
            port_mapping = self.client.getspecificportmapping(external_port, protocol)
            while port_mapping is None and external_port < 65536:
                external_port += 1
                port_mapping = self.client.getspecificportmapping(external_port, protocol)

            success = self.client.addportmapping(external_port, protocol, self.internal_ip, internal_port, label, '')

            if success:
                log.info('Successful %s redirect: %s:%s -> %s:%s', protocol,
                         self.external_ip, external_port,
                         self.internal_ip, internal_port)
                self.external_port = external_port
            else:
                log.error('Failed to map a port')

        except Exception as e:
            log.error('An unexpected error occurred: %s', e)
