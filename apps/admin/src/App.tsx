import { useEffect } from "react";
import "./App.css";
import { api } from "./api/axios/instance";

function App() {
  const healthCheck = async () => {
    const res = await api({
      url: `/health`,
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    console.log(res);
  };
  useEffect(() => {
    healthCheck();
  }, []);

  return <main></main>;
}

export default App;
