export function getHomedir(): string {
  return getUnixHomedir() || getWindowsHomedir();
}

function getUnixHomedir(): string {
  return Deno.env.get("HOME") ?? "";
}

function getWindowsHomedir(): string {
  return Deno.env.get("USERPROFILE") || getAlternateWindowsHomedir();
}

function getAlternateWindowsHomedir(): string {
  const homedrive = Deno.env.get("HOMEDRIVE");
  const homepath = Deno.env.get("HOMEPATH");
  return homedrive && homepath ? `${homedrive}${homepath}` : "";
}
