export const blockchains = {
  solana: {
    name: 'Solana',
    connect: async () => {
      const provider = window.solana;
      if (!provider) throw new Error('Phantom wallet nie znaleziony');

      const res = await provider.connect();
      return res.publicKey.toString();
    },
  },
};
