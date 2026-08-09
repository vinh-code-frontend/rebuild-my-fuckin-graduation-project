import { useEffect } from "react";
import { api } from "../api/axios/instance";
// import { useTranslation } from "react-i18next";
import { RouterProvider } from "react-router/dom";

import { router } from "@/app/router";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

function App() {
  // const { t } = useTranslation();
  const healthCheck = async () => {
    try {
      const res = await api({
        url: `/health`,
        method: "GET",
        headers: { "Content-Type": "application/json" },
      });
      console.log(res);
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    healthCheck();
  }, []);

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router}></RouterProvider>;
    </QueryClientProvider>
  );
}

export default App;
