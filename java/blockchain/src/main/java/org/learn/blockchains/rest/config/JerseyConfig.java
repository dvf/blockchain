package org.learn.blockchains.rest.config;

import org.glassfish.jersey.server.ResourceConfig;
import org.springframework.context.annotation.Configuration;

@Configuration
public class JerseyConfig extends ResourceConfig {
	public JerseyConfig() {
		packages("org.learn.blockchains.rest.services");
	}
}