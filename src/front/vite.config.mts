import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import * as path from "path";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      api: path.resolve("./src/api"),
      app: path.resolve("./src/app"),
      components: path.resolve("./src/components"),
      constant: path.resolve("./src/constant"),
      hooks: path.resolve("./src/hooks"),
      pages: path.resolve("./src/pages"),
      translations: path.resolve("./src/translations"),
    },
  },
  server: { port: 3000 },
});
