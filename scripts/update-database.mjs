import { logger } from "./logger.mjs";

import { execSync } from "child_process";

const run = () => {
  try {
    const name = process.argv[2] ? ` ${process.argv[2]}` : "";

    const cwd = "apps/api/src/App.Infrastructure";
    const cmd = `dotnet ef database update${name} --startup-project ../App.Api`;

    logger.success(`Running: ${cmd}...`);
    execSync(cmd, { cwd, stdio: "inherit" });
    logger.success(`Database updated successfully!`);
  } catch (error) {
    logger.error("Error while updating database");
  }
};

run();
