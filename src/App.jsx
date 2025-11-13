import { useState } from "react";
import "./App.css";
import { initialLeads } from "./mockLeads";
import LeadList from "./components/LeadList";

function App() {
  const [abaAtiva, setAbaAtiva] = useState("invited"); // invited | accepted
  const [leads, setLeads] = useState(initialLeads);

function handleAccept(id) {
  setLeads((prevLeads) =>
    prevLeads.map((lead) => {
      if (lead.id !== id) return lead;

      // aplica desconto de 10% se preço > 500
      let novoPreco = lead.price;
      if (lead.price > 500) {
        novoPreco = +(lead.price * 0.9).toFixed(2);
      }

      // DEBUG: log simples
      console.log("Clique em Accept para o lead", lead.id);

      // simula envio de e-mail (log + alert)
      const mensagemEmail = `[EMAIL] Enviar e-mail para vendas@empresa.com: lead ${lead.id} aceito por $${novoPreco}`;
      console.log(mensagemEmail);
      alert(mensagemEmail);

      return {
        ...lead,
        status: "accepted",
        price: novoPreco,
      };
    })
  );
}


  function handleDecline(id) {
    setLeads((prevLeads) =>
      prevLeads.map((lead) =>
        lead.id === id ? { ...lead, status: "declined" } : lead
      )
    );
  }

  const invitedLeads = leads.filter((lead) => lead.status === "invited");
  const acceptedLeads = leads.filter((lead) => lead.status === "accepted");

  return (
    <div className="app">
      <header className="app-header">
        <h1>Lead Management</h1>
        <p className="app-subtitle">
          Sistema simples para gerenciar leads (Invited / Accepted)
        </p>
      </header>

      {/* abas no topo */}
      <div className="tabs-container">
        <button
          className={
            abaAtiva === "invited" ? "tab-header active" : "tab-header"
          }
          onClick={() => setAbaAtiva("invited")}
        >
          Invited
        </button>

        <button
          className={
            abaAtiva === "accepted" ? "tab-header active" : "tab-header"
          }
          onClick={() => setAbaAtiva("accepted")}
        >
          Accepted
        </button>
      </div>

      <main className="tab-content">
        {abaAtiva === "invited" && (
          <LeadList
            leads={invitedLeads}
            onAccept={handleAccept}
            onDecline={handleDecline}
            showContactDetails={false}
          />
        )}

        {abaAtiva === "accepted" && (
          <LeadList
            leads={acceptedLeads}
            onAccept={null}
            onDecline={null}
            showContactDetails={true}
          />
        )}
      </main>
    </div>
  );
}

export default App;
