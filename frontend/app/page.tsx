import Link from "next/link";

import { buttonVariants } from "@/components/ui/button";

export default function Home() {
  return (
    <main className="flex flex-1">
      <section className="mx-auto flex w-full max-w-6xl flex-1 px-6 py-14 sm:px-8 sm:py-20">
        <div className="grid w-full gap-10 lg:grid-cols-[minmax(0,1.2fr)_minmax(18rem,24rem)] lg:items-start">
          <div className="space-y-8">
            <div className="space-y-4">
              <p className="text-sm font-medium uppercase tracking-[0.2em] text-muted-foreground">
                Home
              </p>
              <h2 className="max-w-xl text-xl font-semibold tracking-tight text-balance sm:text-xl">
                See where you can charge before the battery becomes the problem.
              </h2>
              <p className="max-w-2xl text-base leading-7 text-muted-foreground sm:text-lg">
                Charging Stations is a simple overview for EV drivers. It brings
                nearby charging locations, current availability, and stop planning
                into one clear place.
              </p>
              <Link href="/dashboard" className={buttonVariants({ size: "lg" })}>
                Open dashboard
              </Link>
            </div>
          </div>

          <aside className="rounded-3xl border border-border/70 bg-card/70 p-6 shadow-sm">
            <div className="space-y-5">
              <div>
                <p className="text-sm font-medium text-muted-foreground">
                  What the app does
                </p>
                <p className="mt-2 text-base leading-7 text-card-foreground">
                  It helps drivers find useful stations fast, check whether a stop
                  looks viable, and make better charging decisions before leaving.
                </p>
              </div>
              <div className="space-y-3 border-t border-border/70 pt-5">
                <div>
                  <h3 className="text-sm font-semibold text-foreground">
                    Find stations
                  </h3>
                  <p className="mt-1 text-sm leading-6 text-muted-foreground">
                    Browse charging locations without digging through multiple apps.
                  </p>
                </div>
                <div>
                  <h3 className="text-sm font-semibold text-foreground">
                    Check availability
                  </h3>
                  <p className="mt-1 text-sm leading-6 text-muted-foreground">
                    See station status quickly so each stop is easier to judge.
                  </p>
                </div>
                <div>
                  <h3 className="text-sm font-semibold text-foreground">
                    Plan ahead
                  </h3>
                  <p className="mt-1 text-sm leading-6 text-muted-foreground">
                    Keep charging part of the trip predictable instead of reactive.
                  </p>
                </div>
              </div>
            </div>
          </aside>
        </div>
      </section>
    </main>
  );
}
