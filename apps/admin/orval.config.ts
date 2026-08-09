import { defineConfig } from "orval";

export default defineConfig({
  api: {
    input: {
      target: "http://localhost:5073/openapi/v1.json",
    },

    output: {
      mode: "tags-split",
      target: "./src/api/generated",
      schemas: "./src/api/generated/model",
      client: "react-query",
      httpClient: "axios",

      override: {
        mutator: {
          path: "./src/api/axios/instance.ts",
          name: "api",
        },
      },
    },
  },
});
