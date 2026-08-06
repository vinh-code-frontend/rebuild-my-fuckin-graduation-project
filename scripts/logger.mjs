export const logger = {
  error(message) {
    console.log(`\x1b[31m${message}\x1b[0m`);
  },

  success(message) {
    console.log(`\x1b[32m${message}\x1b[0m`);
  },

  warn(message) {
    console.log(`\x1b[33m${message}\x1b[0m`);
  },
};
