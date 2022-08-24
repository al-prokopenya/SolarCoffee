import { shallowMount } from "@vue/test-utils";
import SolarButton from "@/components/SolarButton.vue";

describe("SolarButton.vue", () => {
  it("displays the text in slot position", () => {
    const wrapper = shallowMount(SolarButton, {
      propsData: {},
      slots: {
        default: "click here!",
      },
    });

    expect(wrapper.find("button").text()).toEqual("click here!");
  });

  it("disabled when it passed in prop", () => {
    // @ts-ignore
    const some = shallowMount(SolarButton, {
      propsData: {
        disabled: true,
      },
      slots: {
        default: "foo",
      },
    });

    expect(some.find("button:disabled"));
  });

  it("enabled when it disabled false passed in prop", () => {
    // @ts-ignore
    const wrapper = shallowMount(SolarButton, {
      propsData: {
        disabled: false,
      },
      slots: {
        default: "foo",
      },
    });

    expect(!wrapper.find("button:disabled"));
  });
});
