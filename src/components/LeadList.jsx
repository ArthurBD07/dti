// src/components/LeadList.jsx
import LeadCard from "./LeadCard";

export default function LeadList({
  leads,
  onAccept,
  onDecline,
  showContactDetails,
}) {
  if (!leads.length) {
    return <p>Não há leads nessa aba.</p>;
  }

  return (
    <div className="lead-list">
      {leads.map((lead) => (
        <LeadCard
          key={lead.id}
          lead={lead}
          onAccept={onAccept}
          onDecline={onDecline}
          showContactDetails={showContactDetails}
        />
      ))}
    </div>
  );
}
