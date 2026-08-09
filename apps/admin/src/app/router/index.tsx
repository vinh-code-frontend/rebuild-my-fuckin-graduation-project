import { createBrowserRouter } from "react-router";
import NotFoundPage from "@features/errors/NotFoundPage";

export const router = createBrowserRouter([
  {
    path: "/login",
    // Component: LoginPage,
  },
  {
    path: "/",
    element: <h1>"/" page</h1>,
    children: [
      {
        index: true,
        element: <h2>dashboard</h2>,
      },
      {
        path: "products",
        element: <h2>products</h2>,
      },
      // 404
    ],
  },
  {
    path: "*",
    Component: NotFoundPage,
  },
]);
