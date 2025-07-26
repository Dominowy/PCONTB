import { AnchorProvider, Program, web3 } from "@project-serum/anchor";
import idl from "./idl.json";
import { Connection, PublicKey } from "@solana/web3.js";

const PROGRAM_ID = new PublicKey("9i9upTEy57inL4ZptrYmcyAnCpo3WwghAJosFhmSZWJB");

const connection = new Connection("https://api.devnet.solana.com", "confirmed");

// Phantom (lub inny) provider
function getProvider() {
  const provider = window.solana;
  return new AnchorProvider(connection, provider, {
    commitment: "confirmed",
  });
}

function getProgram() {
  return new Program(idl, PROGRAM_ID, getProvider());
}

export default {
  getProgram,
  web3,
  PROGRAM_ID,
  connection,
};
