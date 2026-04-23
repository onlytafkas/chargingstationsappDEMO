"use client";

import { useActionState, useEffect, useRef, useState, startTransition } from "react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { createSession } from "@/lib/session-actions";
import {
  initialCreateSessionFormState,
} from "@/lib/session-actions-types";

function getDefaultStartDateTime() {
  const d = new Date(Date.now() + 5 * 60 * 1000);
  // Format as local datetime-local value: YYYY-MM-DDTHH:mm
  const pad = (n: number) => String(n).padStart(2, "0");
  return (
    `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}` +
    `T${pad(d.getHours())}:${pad(d.getMinutes())}`
  );
}

export function CreateSessionModal() {
  const [open, setOpen] = useState(false);
  const [startDateTime, setStartDateTime] = useState(getDefaultStartDateTime);
  const formRef = useRef<HTMLFormElement>(null);

  const [state, formAction, isPending] = useActionState(
    createSession,
    initialCreateSessionFormState
  );

  // Reset and close on success
  useEffect(() => {
    if (state.success) {
      startTransition(() => {
        setOpen(false);
        setStartDateTime(getDefaultStartDateTime());
      });
      formRef.current?.reset();
    }
  }, [state.success]);

  // Refresh start default when dialog opens
  function handleOpenChange(value: boolean) {
    if (value) {
      setStartDateTime(getDefaultStartDateTime());
    }
    setOpen(value);
  }

  // Compute the minimum datetime-local value (now, to nearest minute)
  const minDateTime = (() => {
    const now = new Date();
    const pad = (n: number) => String(n).padStart(2, "0");
    return (
      `${now.getFullYear()}-${pad(now.getMonth() + 1)}-${pad(now.getDate())}` +
      `T${pad(now.getHours())}:${pad(now.getMinutes())}`
    );
  })();

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      <DialogTrigger render={<Button />}>New session</DialogTrigger>
      <DialogContent className="sm:max-w-md">
        <DialogHeader>
          <DialogTitle>Create charging session</DialogTitle>
          <DialogDescription>
            Fill in the details to schedule a new charging session.
          </DialogDescription>
        </DialogHeader>

        <form ref={formRef} action={formAction} className="space-y-5 pt-2">
          <div className="space-y-2">
            <Label htmlFor="userEmail">User email</Label>
            <Input
              id="userEmail"
              name="userEmail"
              type="email"
              placeholder="user@example.com"
              required
              autoComplete="email"
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="stationId">Station ID</Label>
            <Input
              id="stationId"
              name="stationId"
              type="text"
              placeholder="STATION-001"
              required
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="startDateTime">Start date &amp; time</Label>
            <Input
              id="startDateTime"
              name="startDateTime"
              type="datetime-local"
              required
              min={minDateTime}
              value={startDateTime}
              onChange={(e) => setStartDateTime(e.target.value)}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="endDateTime">End date &amp; time</Label>
            <Input
              id="endDateTime"
              name="endDateTime"
              type="datetime-local"
              required
              min={startDateTime}
            />
          </div>

          {state.error && (
            <p className="text-sm text-destructive">{state.error}</p>
          )}

          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => setOpen(false)}
              disabled={isPending}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={isPending}>
              {isPending ? "Saving…" : "Create session"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
