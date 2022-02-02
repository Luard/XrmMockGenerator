import { XrmModel } from "../../__forms__/lead/lead.model";
import B2BKAM from "../../ts/entities/lead/lead";
import { EventContextMock, FormContextMock, XrmMockGenerator } from "xrm-mock";

const builder = new XrmModel.ModelBuilder(new XrmModel.lead());
builder.selectForm("Web Acquisition Form");
builder.buildModel();

const context = XrmMockGenerator.eventContext;

describe("Lead Test", () => {
    beforeAll(() => {});
    beforeEach(() => {});

    it("Lead on load", async () => {
        await B2BKAM.Lead.onLoad(context);
    });

    /*
    it("on save check duplicity", async () => {
        const duplicateCustomerValidation = jest.spyOn(B2BKAM.Lead, "duplicateCustomerValidation");
        await B2BKAM.Lead.onSave(context);
        expect(duplicateCustomerValidation).toHaveBeenCalledTimes(1);
    });
    */
    /*
    it("new_customerwithpotential on change", async () => {
        const onCustomerWithPotentialChange = jest.spyOn(B2BKAM.Lead, "onCustomerWithPotentialChange");
        await B2BKAM.Lead.onLoad(context);
        Xrm.Page.getAttribute("new_customerwithpotential").fireOnChange();
        expect(onCustomerWithPotentialChange).toHaveBeenCalledTimes(1);
    });
    */
    it("on country change fire taxId change", async () => {
        const TaxIdOnChange = jest.spyOn(B2BKAM.Lead, "TaxIdOnChange");
        Xrm.Page.getAttribute("new_taxid").addOnChange(
            B2BKAM.Lead.TaxIdOnChange
        );
        await B2BKAM.Lead.CountryOnChange(context);
        expect(TaxIdOnChange).toHaveBeenCalledTimes(1);
    });
});
