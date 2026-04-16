import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Charging Stations",
  description: "Find charging stations and understand availability before you drive.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html
      lang="en"
      className={`${geistSans.variable} ${geistMono.variable} dark h-full antialiased`}
    >
      <body
        className={`${geistSans.className} flex min-h-full flex-col bg-background font-sans text-foreground`}
      >
        <header className="border-b border-border/60 bg-background/95">
          <div className="mx-auto flex w-full max-w-6xl flex-col gap-2 px-6 py-5 sm:flex-row sm:items-end sm:justify-between sm:px-8">
            <div className="space-y-1">
              <p className="text-sm font-medium text-muted-foreground">
                Charging Stations
              </p>
              <h1 className="text-xl font-semibold tracking-tight text-foreground">
                EV charging, without the guesswork.
              </h1>
            </div>
            <p className="max-w-md text-sm leading-6 text-muted-foreground sm:text-right">
              This app helps drivers find charging stations, review availability,
              and plan stops with less uncertainty.
            </p>
          </div>
        </header>
        {children}
      </body>
    </html>
  );
}
