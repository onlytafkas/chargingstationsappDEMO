export type CreateSessionFormState = {
  success: boolean;
  error: string | null;
};

export const initialCreateSessionFormState: CreateSessionFormState = {
  success: false,
  error: null,
};

export type CreateSessionPayload = {
  userEmail: string;
  startDateTime: string;
  endDateTime: string;
  stationId: string;
};
