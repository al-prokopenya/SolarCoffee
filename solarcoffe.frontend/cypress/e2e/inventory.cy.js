context("Inventory", () => {
  beforeEach(() => {
    cy.visit("http://localhost:8080/");
  });

  it("is inventory page", () => {
    cy.get("#inventoryTitle").contains("Inventory");
  });

  it("has buttons to add inventory and recieve shipment", () => {
    cy.get("#addNewBtn > .solar-button").contains("Add new item");
    cy.get("#recieveShipmentBtn > .solar-button").contains("Recieve Shipment");
  });

  it("has add new modal after click", () => {
    cy.get("#addNewBtn").click();
    cy.get("#modalTitle").contains("New product");
    cy.get("[aria-label='Close modal'] > button").click();
  });

  it("adding new product and closing modal before save new product", () => {
    cy.get("#addNewBtn").click();
    cy.get("#isTaxable").click();
    cy.get("#productName").clear();
    cy.get("#productName").type("Test product", { delay: 50 });

    cy.get("#productDesc").clear();
    cy.get("#productDesc").type("A great new product ", { delay: 50 });

    cy.get("#productPrice").clear();
    cy.get("#productPrice").type("120", { delay: 50 });

    cy.get("[aria-label='Close modal'] > button").click();
  });

  it("adding new product and saving it", () => {
    cy.get("#addNewBtn").click();
    cy.get("#isTaxable").click();
    cy.get("#productName").clear();
    cy.get("#productName").type("Test product 123", { delay: 50 });

    cy.get("#productDesc").clear();
    cy.get("#productDesc").type("A great new product ", { delay: 50 });

    cy.get("#productPrice").clear();
    cy.get("#productPrice").type("120", { delay: 50 });

    cy.get("[aria-label='save new product'] > button").click();
    cy.get("table").contains("td", "Test product 123");
  });

  it("adding new product and archive it", () => {
    cy.get("#addNewBtn").click();
    cy.get("#isTaxable").click();
    cy.get("#productName").clear();
    cy.get("#productName").type("zzTest Archive", { delay: 50 });

    cy.get("#productDesc").clear();
    cy.get("#productDesc").type("some description", { delay: 50 });

    cy.get("#productPrice").clear();
    cy.get("#productPrice").type("120", { delay: 50 });

    cy.get("[aria-label='save new product'] > button").click();

    cy.get("table").contains("td", "zzTest Archive");

    cy.get("#inventory-table > tr > td > div").last().click();

    cy.get("table").contains("td", "zzTest Archive").should("not.exist");
  });
});
