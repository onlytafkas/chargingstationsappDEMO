import { getSessions, type LoadingSession } from "@/lib/sessions";
import { CreateSessionModal } from "@/components/create-session-modal";

const dateTimeFormatter = new Intl.DateTimeFormat("en", {
  dateStyle: "medium",
  timeStyle: "short",
});

function formatSessionDateTime(value: string) {
  return dateTimeFormatter.format(new Date(value));
}

export default async function DashboardPage() {
  let sessions: LoadingSession[] = [];
  let errorMessage: string | null = null;

  try {
    sessions = await getSessions();
  } catch (error) {
    errorMessage =
      error instanceof Error ? error.message : "Unable to load sessions.";
  }

  return (
    <main className="flex flex-1">
      <section className="mx-auto flex w-full max-w-6xl flex-1 px-6 py-14 sm:px-8 sm:py-20">
        <div className="w-full space-y-8">
          <div className="space-y-3">
            <p className="text-sm font-medium uppercase tracking-[0.2em] text-muted-foreground">
              Dashboard
            </p>
            <div className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between">
              <div className="space-y-2">
                <h1 className="text-3xl font-semibold tracking-tight text-foreground">
                  Charging sessions
                </h1>
                <p className="max-w-2xl text-base leading-7 text-muted-foreground">
                  Review all recorded charging sessions from the existing backend
                  endpoint.
                </p>
              </div>
              <CreateSessionModal />
            </div>
          </div>

          {errorMessage ? (
            <div className="rounded-3xl border border-destructive/40 bg-destructive/10 p-6 text-sm leading-6 text-destructive">
              {errorMessage}
            </div>
          ) : sessions.length === 0 ? (
            <div className="rounded-3xl border border-border/70 bg-card/70 p-6 shadow-sm">
              <p className="text-base font-medium text-card-foreground">
                No sessions found.
              </p>
              <p className="mt-2 text-sm leading-6 text-muted-foreground">
                The backend returned an empty list for charging sessions.
              </p>
            </div>
          ) : (
            <ul className="space-y-4">
              {sessions.map((session) => (
                <li
                  key={session.id}
                  className="rounded-3xl border border-border/70 bg-card/70 p-6 shadow-sm"
                >
                  <div className="flex flex-col gap-5 lg:flex-row lg:items-start lg:justify-between">
                    <div className="space-y-3">
                      <div>
                        <p className="text-sm font-medium text-muted-foreground">
                          User
                        </p>
                        <p className="mt-1 text-lg font-semibold tracking-tight text-card-foreground">
                          {session.userEmail}
                        </p>
                      </div>
                      <div>
                        <p className="text-sm font-medium text-muted-foreground">
                          Station
                        </p>
                        <p className="mt-1 text-sm leading-6 text-card-foreground">
                          {session.stationId}
                        </p>
                      </div>
                    </div>

                    <dl className="grid gap-4 text-sm leading-6 text-muted-foreground sm:grid-cols-2 lg:min-w-[22rem]">
                      <div>
                        <dt className="font-medium text-card-foreground">Started</dt>
                        <dd className="mt-1">{formatSessionDateTime(session.startDateTime)}</dd>
                      </div>
                      <div>
                        <dt className="font-medium text-card-foreground">Ended</dt>
                        <dd className="mt-1">{formatSessionDateTime(session.endDateTime)}</dd>
                      </div>
                    </dl>
                  </div>
                </li>
              ))}
            </ul>
          )}
        </div>
      </section>
    </main>
  );
}