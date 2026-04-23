"use server";

import { revalidatePath } from "next/cache";
import type { CreateSessionFormState, CreateSessionPayload } from "./session-actions-types";

const defaultBackendBaseUrl = "http://localhost:5197";

function getBackendBaseUrl() {
  return process.env.BACKEND_BASE_URL ?? defaultBackendBaseUrl;
}

export async function createSession(
  _prev: CreateSessionFormState,
  formData: FormData
): Promise<CreateSessionFormState> {
  const userEmail = formData.get("userEmail")?.toString().trim() ?? "";
  const startDateTime = formData.get("startDateTime")?.toString().trim() ?? "";
  const endDateTime = formData.get("endDateTime")?.toString().trim() ?? "";
  const stationId = formData.get("stationId")?.toString().trim() ?? "";

  if (!userEmail || !startDateTime || !endDateTime || !stationId) {
    return { success: false, error: "All fields are required." };
  }

  const payload: CreateSessionPayload = {
    userEmail,
    startDateTime,
    endDateTime,
    stationId,
  };

  let response: Response;
  try {
    response = await fetch(`${getBackendBaseUrl()}/api/sessions`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(payload),
    });
  } catch {
    return { success: false, error: "Unable to reach the server. Please try again." };
  }

  if (!response.ok) {
    let errorMessage = `Request failed (${response.status}).`;
    try {
      const body = (await response.json()) as { error?: string };
      if (body.error) errorMessage = body.error;
    } catch {
      // ignore JSON parse errors
    }
    return { success: false, error: errorMessage };
  }

  revalidatePath("/dashboard");
  return { success: true, error: null };
}
