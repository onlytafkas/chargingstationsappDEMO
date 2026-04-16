---
description: UI rules for the React frontend.
applyTo: frontend/**
---

# UI Guidelines

- The frontend is built with React and Next.js (App Router).
- All UI components MUST use shadcn/ui.
- Custom UI components outside shadcn/ui are NOT allowed.
- The application MUST use dark mode.
- The application MUST use Geist fonts.
- Styling is done with Tailwind CSS only.

## Dark mode

- Dark mode is enforced by placing the `dark` class on the root `<html>` element — NOT via a toggle or `prefers-color-scheme`.
- The `<body>` MUST use semantic color tokens (`bg-background text-foreground`), never hardcoded colors like `bg-zinc-950`.

## Geist fonts

- Geist Sans and Geist Mono MUST be loaded via `next/font/google` and their CSS variables (`--font-geist-sans`, `--font-geist-mono`) applied to `<html>`.
- The app MUST visually render in Geist Sans for normal UI text. If the rendered page looks like a serif or editorial display font, that is a failure and the implementation is wrong.
- The `<body>` MUST include `font-sans` so the Geist variable is used as the document default. Without it the browser falls back to its system font.
- The `<body>` SHOULD also include `geistSans.className` in addition to the font variable classes to make the Geist Sans font application explicit at runtime.
- Do NOT introduce or preserve decorative, serif, editorial, or display-style typography for headings, hero copy, buttons, cards, or navigation unless the user explicitly asks for a different font direction. By default, headings and body text should both render in Geist Sans.
- Do NOT assume that loading the font variable alone is enough; verify the actual rendered result. If the page does not visibly look like Geist Sans, fix the implementation instead of assuming compliance.
- In `globals.css`, the `@theme inline` block MUST map `--font-sans` to `var(--font-geist-sans)` (NOT `var(--font-sans)` — that is circular and resolves to nothing):

```css
@theme inline {
  --font-sans: var(--font-geist-sans);
  --font-mono: var(--font-geist-mono);
}
```

- Required implementation pattern:

```tsx
const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

const geistMono = Geist_Mono({
  variable: '--font-geist-mono',
  subsets: ['latin'],
});

<html className={`${geistSans.variable} ${geistMono.variable} dark`}>
  <body className={`${geistSans.className} font-sans bg-background text-foreground`}>
```

- Third-party components that render outside the normal DOM tree must be explicitly told to use the font variable via their own `appearance`/`style` props — CSS inheritance does NOT cross their portal boundary automatically.

## Stacking contexts

- Do NOT use `backdrop-filter` on layout elements (e.g. headers) unless you are certain no modal or overlay portal needs to render above them. `backdrop-filter` creates a new stacking context that traps portals behind the element.

If a required UI element does not exist in shadcn/ui, STOP and ask.