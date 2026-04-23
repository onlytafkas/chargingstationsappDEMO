import "server-only";

export type LoadingSession = {
  id: number;
  userEmail: string;
  startDateTime: string;
  endDateTime: string;
  stationId: string;
};

const defaultBackendBaseUrl = "http://localhost:5197";

function getBackendBaseUrl() {
  return process.env.BACKEND_BASE_URL ?? defaultBackendBaseUrl;
}

export async function getSessions() {
  const response = await fetch(`${getBackendBaseUrl()}/api/sessions`, {
    cache: "no-store",
    headers: {
      Accept: "application/json",
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to load sessions (${response.status})`);
  }

  const sessions = (await response.json()) as unknown;

  if (!Array.isArray(sessions)) {
    throw new Error("Sessions response was not a list.");
  }

  return sessions as LoadingSession[];
}