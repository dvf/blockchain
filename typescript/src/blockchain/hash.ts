import { createHash } from 'crypto';

export function hash(block: IBlock): string {
	// Hashes a Block
	// We must make sure that the Dictionary is Ordered, or we'll have inconsistent hashes

	let json = JSON.stringify(block);

	return sha256(json);
}

export function sha256(str: string): string {
	return createHash('SHA256').update(str).digest('hex');
}